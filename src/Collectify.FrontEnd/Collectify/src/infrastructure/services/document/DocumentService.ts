export class DocumentService {
    public handleBodyOverflow(isModalActive: boolean): void {
        if (isModalActive) {
            document.body.classList.add("overflow-hidden");
        } else {
            document.body.classList.remove("overflow-hidden");
        }
    }
}