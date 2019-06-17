const transform = (number) => {
  const isMultipleOfThree = number % 3 === 0
  const isMultipleOfFive = number % 5 === 0

  if (isMultipleOfThree && isMultipleOfFive) {
    return "fizzbuzz"
  }

  if (isMultipleOfThree) {
    return "fizz"
  }
  
  if (isMultipleOfFive) {
    return "buzz"
  }

  return number.toString()
}

const fizzer = (numbers) => numbers.map(transform).join(" ")

module.exports = fizzer
