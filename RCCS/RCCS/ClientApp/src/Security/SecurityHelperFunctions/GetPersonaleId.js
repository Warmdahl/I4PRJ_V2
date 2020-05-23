export function getPersonaleId() {
    const jwt = localStorage.getItem('jwt')
    let personaleId;
    try {
        if (jwt) {
            let jwtData = jwt.split('.')[1]
            let decoded = window.atob(jwtData)
            let decodedData = JSON.parse(decoded)
            personaleId =  (decodedData['personaleIdClearText'])
            return personaleId;
        }
    } catch (error) {
        console.log(error)
    }
}
