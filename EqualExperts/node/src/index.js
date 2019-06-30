const app = require("./app")

const numbers = [...Array(21).keys()].splice(1)
const output = app(numbers)

console.log(output)
