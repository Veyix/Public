const fizzer = require("./fizzer")

const numbers = [...Array(21).keys()].splice(1)
const output = fizzer(numbers)

console.log(output)
