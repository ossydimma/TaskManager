/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.razor",
        "./**/*.html",
        "./**/*.cshtml",
    ],
    theme: {
        extend: {
            fontFamily : {
                sans: ['Roboto', 'Arial', 'sans-serif'],
            }
        },
    },
    plugins: [],
}


