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