const { tracker, getReport } = require("./tracker")
const fizzer = require("./fizzer")

const numbers = [...Array(21).keys()].splice(1)
const output = fizzer(numbers, tracker)
const report = getReport()

console.log(`${output} ${report}`)
