This folder must contain a file called `tokens.ts` to store the security/configuration tokens to use some services.

This is a sample content for this file:

```ts
// Get the Firebase tokens from the Firebase console and set them this way:
export const tokens = {
    firebase: {
        apiKey: '<your-key>',
        authDomain: '<your-project-authdomain>',
        databaseURL: '<your-database-URL>',
        projectId: '<your-project-id>',
        storageBucket: '<your-storage-bucket>',
        messagingSenderId: '<your-messaging-sender-id>',
        appId: '<your-registered-app-id>',
    },
};
```
