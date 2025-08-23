# Nginx Server Configuration Instructions for Unity WebGL Applications

To ensure optimal performance and avoid warnings in the console, the Nginx server must send files with the `.wasm` extension with the correct Content-Type header.

There are two recommended methods to achieve this. Choose one.

---

### Method 1: Edit the global `mime.types` file (Recommended)

This is the cleanest method and will work for all sites on the server.

1. Log in to your server and locate the `mime.types` file. It is usually found at `/etc/nginx/mime.types`.

2. Open the file for editing and inside the `types { ... }` block, add the following line:

    ```nginx
    application/wasm    wasm;
    ```

    Example:
    ```nginx
    types {
        text/html                             html htm shtml;
        text/css                              css;
        text/xml                              xml;
        application/javascript                js;
        application/wasm                      wasm;
        # ... other types
    }
    ```

3. Save the file and reload the Nginx configuration using:
    `sudo systemctl reload nginx` or `nginx -s reload`

---

### Method 2: Add a rule in your site's configuration

If you do not have access to the global `mime.types` file, you can add the type definition directly in the `server` block for your site.

1. Open your site's configuration file (e.g., in `/etc/nginx/sites-available/your-domain.conf`).

2. Inside the `server { ... }` block, add a small `types` block:

    ```nginx
    server {
        listen 80;
        server_name your-domain.com;
        root /var/www/your-site;

        # ... other directives ...

        # Add this block to define the MIME type for .wasm
        types {
            application/wasm wasm;
        }

        location / {
            try_files $uri $uri/ /index.html;
        }
    }
    ```

3. Save the file and reload the Nginx configuration.

---
