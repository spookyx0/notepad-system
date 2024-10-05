/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
      './Views/**/*.{cshtml,html}',
      './wwwroot/**/*.html',
      './wwwroot/**/*.js',
  ],
  daisyui: {
    themes: ["light", "dark", "cupcake"],
  },
  theme: {  
      extend: {},
  },
  plugins: [
      require('daisyui'),
  ],
}
