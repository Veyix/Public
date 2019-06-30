const transform = (number) => {
  const numberContainsThree = number.toString().includes("3")
  const isMultipleOfThree = number % 3 === 0
  const isMultipleOfFive = number % 5 === 0

  if (numberContainsThree) {
    return "lucky"
  }

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

const fizzer = (numbers, trackerCallback) => {
  const results = numbers.map(number => {
    
    const result = transform(number)
    trackerCallback(result)

    return result
  })
  
  return results.join(" ")
}

module.exports = fizzer
