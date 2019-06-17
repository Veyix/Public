const isMultipleOfThree = (number) => number % 3 === 0
const isMultipleOfFive = (number) => number % 5 === 0
const isLastNumber = (currentIndex, numberCount) => currentIndex === numberCount - 1

const fizzer = (numbers) => {
  let output = ""

  numbers.map((number, index) => {
    const isThree = isMultipleOfThree(number)
    const isFive = isMultipleOfFive(number)

    if (isThree && isFive) {
      output += "fizzbuzz"
    }
    else if (isThree) {
      output += "fizz"
    }
    else if (isFive) {
      output += "buzz"
    }
    else {
      output += number.toString()
    }

    if (!isLastNumber(index, numbers.length)) {
      output += " "
    }
  })

  return output
}

module.exports = fizzer
