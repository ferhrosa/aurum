import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AngularFirestore, QueryFn } from '@angular/fire/firestore';
import * as firebase from 'firebase';

import { environment } from 'src/environments/environment';

import { Entity } from '../model/entity.model';


const root = `environments/${environment.firebaseEnvironment}/`;

export abstract class BaseService<T extends Entity> {

    protected constructor(
        protected db: AngularFirestore,
        protected collectionPath: string) { }

    protected static toSaveable<T extends Entity>(entity: T): T {
        const saveable = Object.assign({}, entity as T);

        const convertArrays = (object) => {
            // tslint:disable-next-line:forin
            for (const item in object) {
                if (typeof (object[item]) === 'object') {
                    convertArrays(object[item]);
                }

                if (Array.isArray(object[item]) && typeof (object[item][0]) === 'object') {
                    object[item] = (object[item] as Array<any>).map(obj => Object.assign({}, obj));
                }

                if (object[item] instanceof Date) {
                    object[item] = firebase.firestore.Timestamp.fromDate(object[item]);
                }
            }
        };

        convertArrays(saveable);

        delete saveable.id;
        return saveable;
    }

    protected getFullCollectionPath = (): string => `${root}${this.collectionPath}`;

    public getCollection(queryFn?: QueryFn): Observable<T[]> {
        return this.db.collection<T>(this.getFullCollectionPath(), queryFn).snapshotChanges().pipe(map(
            actions => actions.map(
                a => {
                    const entity = a.payload.doc.data() as T;
                    entity.id = a.payload.doc.id;

                    this.executeCustomMappings(a.payload.doc.data(), entity);

                    return entity;
                }
            )
        ));
    }

    public getEntity(id: string): Observable<T> {
        return this.db.collection<T>(this.getFullCollectionPath()).doc<T>(id).valueChanges().pipe(map(
            doc => {
                const entity = doc as T;
                entity.id = id;

                this.executeCustomMappings(doc, entity);

                return entity;
            }
        ));
    }

    protected abstract executeCustomMappings(doc: any, entity: T);

    public async save(entity: T) {
        const toSave = BaseService.toSaveable(entity);

        if (entity.id) {
            await this.db.collection(this.getFullCollectionPath())
                .doc(entity.id)
                .update(toSave);
        } else {
            await this.db.collection(this.getFullCollectionPath())
                .add(toSave);
        }
    }

}
