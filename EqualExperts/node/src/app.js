const { reset, tracker, getReport } = require("./tracker")
const fizzer = require("./fizzer")

const app = (numbers) => {
  reset()

  const output = fizzer(numbers, tracker)
  const report = getReport()

  return `${output} ${report}`
}

module.exports = app