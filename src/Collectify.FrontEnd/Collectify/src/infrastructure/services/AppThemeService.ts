export class AppThemeService {
    public isDarkMode(): boolean {
        if(localStorage.getItem("darkMode") !== null)
            return localStorage.getItem("darkMode") === "true";

        return window.matchMedia("(prefers-color-scheme: dark)").matches;
    }

    public toggleDarkMode(isDarkMode: boolean): void {
        document.documentElement.classList.toggle("dark", isDarkMode);
        const darkMode = localStorage.getItem("darkMode") !== null ? localStorage.getItem("darkMode") == "true" : false;
        localStorage.setItem("darkMode", (!darkMode).toString());
    }   

    public setAppTheme(): void {
        if (this.isDarkMode()) {
            document.documentElement.classList.add("dark");
        } else {
            document.documentElement.classList.remove("dark");
        }

        localStorage.setItem("darkMode", this.isDarkMode().toString());
    }
}