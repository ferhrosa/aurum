package apk.aurum;

import java.util.ArrayList;
import java.util.Calendar;

import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.Spinner;
import android.widget.TextView;
import apk.aurum.BancoDados.CategoriasCursor;

public class AurumAndroidActivity extends Activity {

	private TextView txtDataSelecionada, txtNome;
	private Button bntSelecionarData, btnNovaCategoria, btnCancelar, btnSalvar;
	private int intAno, intMes, intDia;

	static final int intControlaDialog = 0;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		CarregarViewPrincipal();

	}

	private void atualizaData() {
		txtDataSelecionada.setText(new StringBuilder().append(intDia)
				.append("/").append(intMes + 1).append("/").append(intAno)
				.append(" "));
	}

	private DatePickerDialog.OnDateSetListener mDateSetListener = new DatePickerDialog.OnDateSetListener() {

		public void onDateSet(DatePicker view, int year, int monthOfYear,
				int dayOfMonth) {

			intAno = year;
			intMes = monthOfYear;
			intDia = dayOfMonth;

			atualizaData();
		}

	};

	@Override
	protected Dialog onCreateDialog(int id) {
		switch (id) {
		case intControlaDialog:
			return new DatePickerDialog(this, mDateSetListener, intAno, intMes,
					intDia);
		}
		return null;
	}

	public void CarregarViewPrincipal() {
		setContentView(R.layout.main);

		txtDataSelecionada = (TextView) findViewById(R.id.txtDataSelecionada);
		bntSelecionarData = (Button) findViewById(R.id.btnSelecionarData);
		btnNovaCategoria = (Button) findViewById(R.id.btnNovaCategoria);

		// Configurando o botão "Selecionar Data"
		bntSelecionarData.setOnClickListener(new View.OnClickListener() {

			public void onClick(View v) {
				showDialog(intControlaDialog);
			}

		});

		final Calendar c = Calendar.getInstance();
		intAno = c.get(Calendar.YEAR);
		intMes = c.get(Calendar.MONTH);
		intDia = c.get(Calendar.DAY_OF_MONTH);

		atualizaData();

		// Configurando o botão "Nova Categoria"
		btnNovaCategoria.setOnClickListener(new View.OnClickListener() {

			public void onClick(View v) {
				CarregarViewCadastro();
			}

		});

		CarregarLista(this);
	}

	public void CarregarViewCadastro() {
		setContentView(R.layout.categoria);

		btnCancelar = (Button) findViewById(R.id.btnCancelar);
		btnSalvar = (Button) findViewById(R.id.btnSalvar);
		txtNome = (TextView) findViewById(R.id.txtNome);

		// Configurando o botão "Cancelar"
		btnCancelar.setOnClickListener(new View.OnClickListener() {

			public void onClick(View v) {
				CarregarViewPrincipal();
			}

		});

		// Configurando o botão "Salvar"
		btnSalvar.setOnClickListener(new View.OnClickListener() {

			public void onClick(View v) {
				InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
				imm.hideSoftInputFromWindow(txtNome.getWindowToken(), 0);

				SalvarCadastro();
			}

		});

	}

	public void SalvarCadastro() {

		BancoDados db = new BancoDados(this);
		db.InserirCategoria(txtNome.getText().toString());
		CarregarViewPrincipal();

	}

	public void CarregarLista(Context c) {
		BancoDados db = new BancoDados(c);
		CategoriasCursor cursor = db
				.RetornarCategorias(CategoriasCursor.OrdenarPor.NomeCrescente);

		ArrayList<String> Categorias = new ArrayList<String>();

		for (int i = 0; i < cursor.getCount(); i++) {
			cursor.moveToPosition(i);
			Categorias.add(cursor.getNome());
		}

		Spinner spnCategoria = new Spinner(this);

		ArrayAdapter<String> CategoriasAdapter = new ArrayAdapter<String>(this,
				android.R.layout.simple_spinner_item, Categorias);

		CategoriasAdapter
				.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);

		spnCategoria = (Spinner) findViewById(R.id.spinner1);
		spnCategoria.setAdapter(CategoriasAdapter);
	}
}