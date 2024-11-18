/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.razor",
        "./**/*.html",
        "./**/*.cshtml",
    ],
    theme: {
        extend: {
            screens : {
                xxs: "400px",
                xs: "535px",
                sm: "600px",
                md: "815px"
            },
           
            fontFamily : {
                sans: ['Roboto', 'Arial', 'sans-serif'],
            }
        },
    },
    plugins: [],
}


