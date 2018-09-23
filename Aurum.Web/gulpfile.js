/// <binding ProjectOpened='watch' />
var gulp = require('gulp');
var ts = require('gulp-typescript');
var sass = require('gulp-sass');

// Caminho dos fontes da aplicação web, que contém os TypeScripts, SASS e HTML do Angular 2.
var caminhoSrc = './src/';

// Caminho da aplicação web, que contém os componentes do Angular 2 compilados.
var caminhoApp = './app/';


// Copia arquivos gerados automaticamente.

var caminhosCopia = [
	'../Aurum.Api.Client/ts/api.service.ts',
	'../Aurum.Api.Client/ts/model.ts',
	`${caminhoSrc}**/*.htm`,
	`${caminhoSrc}**/*.html`
];

gulp.task("copia", () => {
	gulp.src(caminhosCopia)
		.pipe(gulp.dest(caminhoApp));
});


// TypeScript

// Carrega as configurações do arquivo tsconfig.json, para que o compilador sempre
// use as mesmas configurações, independente de onde esteja sendo executado.
var tsProject = ts.createProject('tsconfig.json');

// Caminho/arquivos que serão processados pelo compilador de TypeScript.
var caminhoTs = `${caminhoSrc}**/*.ts`;

// Executa o compilador do TypeScript.
gulp.task('ts', () => {
	gulp.src(caminhoTs)
		.pipe(tsProject())
		.pipe(gulp.dest(caminhoApp));
});



// SASS

// Caminhos/arquivos que serão processados pelo compilador de SASS.
var caminhoSass = `${caminhoSrc}/**/*.scss`;

// Executa o compilador do SASS.
gulp.task('sass', () => {
	gulp.src(caminhoSass)
		.pipe(sass().on('error', sass.logError))
		.pipe(gulp.dest(caminhoApp));
});


// Executa as tasks padrões.
gulp.task('default', ['copia', 'ts', 'sass']);


// Monitora alterações nos arquivos de origem e executa as tasks que os processam.
gulp.task('watch', () => {
	gulp.watch(caminhosCopia, ['copia']);
	gulp.watch(caminhoTs, ['ts']);
	gulp.watch(caminhoSass, ['sass']);
});
