/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
      './Views/**/*.{cshtml,html}',
      './wwwroot/**/*.html',
      './wwwroot/**/*.js',
  ],
  theme: {
      extend: {},
  },
  plugins: [
      require('daisyui'),
  ],
}
