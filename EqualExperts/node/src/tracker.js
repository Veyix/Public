const counters = {
  lucky: 0,
  fizzbuzz: 0,
  fizz: 0,
  buzz: 0,
  integer: 0
}

const reset = () => {
  counters.lucky = 0
  counters.fizzbuzz = 0
  counters.fizz = 0
  counters.buzz = 0
  counters.integer = 0
}

const tracker = (value) => {
  switch (value) {
    case "lucky":
    case "fizzbuzz":
    case "fizz":
    case "buzz":
      counters[value]++
      break;

    default:
      counters.integer++
  }
}

const getReport = () => {
  return [
    `fizz: ${counters.fizz}`,
    `buzz: ${counters.buzz}`,
    `fizzbuzz: ${counters.fizzbuzz}`,
    `lucky: ${counters.lucky}`,
    `integer: ${counters.integer}`
  ].join(" ")
}

module.exports = {
  reset,
  tracker,
  getReport
}