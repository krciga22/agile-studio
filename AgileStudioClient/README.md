## Agile Studio Client
The Agile Studio Client is a web application that serves as the front-end interface for Agile Studio.

## Client Architecture
Agile Studio uses **Typescript** for better type support, **react.js** for 
better modularity and reactivity of UI components, and **Bootstrap** for styling.

### Project Structure
* public/ - Files accessible to the public (HTML, CSS, JS, images)
* src/ - Source code for the project

### Source Structure
* assets/ - Static assets like images, fonts, etc.
* layouts/ - Layout components for consistent structure
  * footers/ - Footer components
  * headers/ - Header components
  * menus/ - Navigation menu components
* models/ - Data models used in the application
* pages/ - Different pages of the application
* services/ - Services for API calls and business logic

### Routing
The application uses **PageRouter** (a custom routing component) for basic routing between pages. 
This allows for quick development of pages, but doesn't allow for server side rendering (SSR) which 
may be a limitation for SEO in the future. Currently, SSR is not a priority as Agile Studio is primarily 
used as an internal tool, but this may change in the future.

### Setting Environment Variables
Environment variables are set in `AgileStudioClient/env.js`. These 
variables are then copied to `AgileStudioClient/public/env.js` whenever you execute `npm run set-env`. You can 
create additional environment files like `env.development.js` 
and use them from the command line (`npm run set-env development`).

Environment variables from node process are not supported at this time, but may be supported in the future.

### Generating a Self-Signed SSL Certificate for Development
```bash
openssl req -x509 -out development-agilestudio-dev.crt -keyout development-agilestudio-dev.key \
  -newkey rsa:2048 -nodes -sha256 \
  -subj '/CN=development.agilestudio.dev' -extensions EXT -config <( \
   printf "[dn]\nCN=development.agilestudio.dev\n[req]\ndistinguished_name = dn\n[EXT]\nsubjectAltName=DNS:development.agilestudio.dev\nkeyUsage=digitalSignature\nextendedKeyUsage=serverAuth")
```