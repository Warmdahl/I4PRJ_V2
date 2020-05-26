export function getLogInState() {
    if (localStorage.getItem("jwt") != null) {
        return true;
    } else {
        return false;
    }
    return false;
}