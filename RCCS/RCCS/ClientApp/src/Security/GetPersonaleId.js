export function getPersonaleId() {
    const jwt = localStorage.getItem('jwt')
    let rolle;
    try {
        if (jwt) {
            let jwtData = jwt.split('.')[1]
            let decoded = window.atob(jwtData)
            let decodedData = JSON.parse(decoded)
            let personaleId =  (decodedData['personaleId'])
        }
    } catch (error) {
        console.log(error)
    }
    return personaleId;
}
