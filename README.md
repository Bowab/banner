# banner

Hej!
Instruktioner hittar ni på startsidan när ni startar appen.

Jag körde .net core 3.1 web application med API som template.

Det går bra att använda appens formulär eller posta med postman mot api:et.

get: /banner?id=1 -- GET

create: /banner/create -- POST: form-data eller raw body json

update: /banner/update -- POST: form-data eller raw body json, kräver id

delete: /banner/delete -- POST: form-data eller raw body json, kräver id

hämta alla banners: /banner/all -- GET


Hade detta varit ett större och riktigare projekt hade jag gjort följande ändringar:

1. Ett riktigt GUI för att skapa banners men lite förvalada templates (färdiga cshtml vyer, med banner som vy-modell). 
Jag hade formaterat vyer till string och lagrat i en databas med hjälp av .net core's ViewEngine, mer info här: 
https://www.codemag.com/article/1312081/Rendering-ASP.NET-MVC-Razor-Views-to-String

2. Använt ett riktigt front-end framework, typ angular och gjort en separat applikation som konsumerade api:et

3. Jag hade lagt in en riktig html-editor, Froala är nice och den har dessutom stöd för att ladda upp bilder till din egna server (front-end delen av applikation.)

Mer info om forala här:

https://froala.com/wysiwyg-editor/docs/concepts/image/upload/

```
new FroalaEditor('.selector', {

    // Set the image upload parameter.
    imageUploadParam: 'image_param',
    
    // Set the image upload URL.
    imageUploadURL: '/upload_image',
    
    });
 ```
 
4. Gjort api-dokumentation med tillexempel Swagger.

5. Och slutligen använt en rikitg databas.
