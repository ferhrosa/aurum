import { FirebaseAppConfig } from '@angular/fire';


const firebasePrefix = 'firebase-';

export class Configurations {

    static config: FirebaseAppConfig = {};

    static isConfigured(): boolean {
        return !!(this.config.apiKey
            && this.config.authDomain
            && this.config.databaseURL
            && this.config.projectId
            && this.config.storageBucket
            && this.config.messagingSenderId);
    }

    public static getFirebaseAppConfig(): FirebaseAppConfig {

        const apiKey = localStorage.getItem(`${firebasePrefix}apiKey`);
        const authDomain = localStorage.getItem(`${firebasePrefix}authDomain`);
        const databaseURL = localStorage.getItem(`${firebasePrefix}databaseURL`);
        const projectId = localStorage.getItem(`${firebasePrefix}projectId`);
        const storageBucket = localStorage.getItem(`${firebasePrefix}storageBucket`);
        const messagingSenderId = localStorage.getItem(`${firebasePrefix}messagingSenderId`);

        this.mapConfig(<FirebaseAppConfig>{ apiKey, authDomain, databaseURL, projectId, storageBucket, messagingSenderId });

        return this.config;
    }

    public static setFirebaseAppConfig(config: FirebaseAppConfig) {
        this.mapConfig(config);

        localStorage.setItem(`${firebasePrefix}apiKey`, config.apiKey);
        localStorage.setItem(`${firebasePrefix}authDomain`, config.authDomain);
        localStorage.setItem(`${firebasePrefix}databaseURL`, config.databaseURL);
        localStorage.setItem(`${firebasePrefix}projectId`, config.projectId);
        localStorage.setItem(`${firebasePrefix}storageBucket`, config.storageBucket);
        localStorage.setItem(`${firebasePrefix}messagingSenderId`, config.messagingSenderId);
    }

    private static mapConfig(config: FirebaseAppConfig) {
        this.config.apiKey = config.apiKey;
        this.config.authDomain = config.authDomain;
        this.config.databaseURL = config.databaseURL;
        this.config.projectId = config.projectId;
        this.config.storageBucket = config.storageBucket;
        this.config.messagingSenderId = config.messagingSenderId;
    }

}
