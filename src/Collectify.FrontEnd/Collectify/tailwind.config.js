/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  darkMode: 'class',
  theme: {
    screens: {
      md: '745px',
      lg: '950px',
      xl: '1440px',
    },

    extend: {
      colors: {
        primaryColor: '#EDEAE5',
        secondaryColor: '#A3E4B7',
        tertiaryColor: '#b2b0ad',
        primaryContentColor: '#000100',
        primaryAlternativeContentColor: '#222222',
        secondaryContentColor: '#b2b0ad',
        secondaryAlternativeContentColor: '#777777',
        accentPrimaryColor: '#FCE181',
        accentSecondaryColor: '#b2b0ad',
        accentTertiaryColor: '#03A9F4',
        primaryBorderColor: '#026670',
        secondaryBorderColor: '#FEF9C7',

        primaryColorDark: '#232429',
        secondaryColorDark: '#333740',
        tertiaryColorDark: '#40434D',
        primaryContentColorDark: '#f5f5f5',
        secondaryContentColorDark: '#b5b5b5',
        tertiaryContentColorDark: '#999fab',
        accentPrimaryColorDark: '#7E7CF7',
        accentSecondaryColorDark: '#656a80',
        accentTertiaryColorDark: '#4B4D63',
        dangerColor: '#E63946',
        successColor: '#2A9D8F'
      },

      fontFamily: {
        'poppins': ['Poppins', 'sans-serif'] 
      },

      keyframes: {
        fadeIn: {
          '0%': { opacity: '0', transform: 'translateY(-50%) translateX(-50%)' },
          '100%': { opacity: '1', transform: 'translateY(-50%) translateX(-50%)' },
        },
        fadeOut: {
          '0%': { opacity: '1', transform: 'translateY(-50%) translateX(-50%)' },
          '100%': { opacity: '0', transform: 'translateY(-50%) translateX(-50%)' },
        },
        backgroundFadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' },
        },
        backgroundFadeOut: {
          '0%': { opacity: '1' },
          '100%': { opacity: '0' },
        },
      },
      animation: {
        fadeIn: 'fadeIn 0.15s ease-out forwards',
        fadeOut: 'fadeOut 0.15s ease-out forwards',
        backgroundFadeIn: 'backgroundFadeIn 0.3s ease-out forwards',
        backgroundFadeOut: 'backgroundFadeOut 0.3s ease-out forwards',
      },
      boxShadow: {
        custom: '0px 10px 30px rgba(0, 0, 0, 0.03)',
      }

  },
  plugins: [],
  }
}
