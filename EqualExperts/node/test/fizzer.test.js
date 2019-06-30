require("chai").should()

const fizzer = require("../src/fizzer")
const trackerCallback = (_) => {}

describe("fizzer", () => {
  it("should output the expected report", () => {

    const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    const output = fizzer(numbers, trackerCallback)

    output.should.equal("1 2 lucky 4 buzz fizz 7 8 fizz buzz")
  })

  it("should output 'fizz' for a multiple of 3", () => {

    const numbers = [6, 9, 12]
    const output = fizzer(numbers, trackerCallback)

    output.should.equal("fizz fizz fizz")
  })

  it("should output 'buzz' for a multiple of 5", () => {

    const numbers = [5, 10, 20]
    const output = fizzer(numbers, trackerCallback)

    output.should.equal("buzz buzz buzz")
  })

  it("should output 'fizzbuzz' for a multiple of both 3 and 5", () => {

    const numbers = [15, 45, 60]
    const output = fizzer(numbers, trackerCallback)

    output.should.equal("fizzbuzz fizzbuzz fizzbuzz")
  })

  it("should output 'lucky' for numbers containing a '3'", () => {

    const numbers = [3, 13, 30]
    const output = fizzer(numbers, trackerCallback)

    output.should.equal("lucky lucky lucky")
  })
})
