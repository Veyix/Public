require("chai").should()

const app = require("../src/app")

describe("app", () => {
  it("should produce the expected output given numbers 1-10", () => {
    const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    const output = app(numbers)

    output.should.equal("1 2 lucky 4 buzz fizz 7 8 fizz buzz fizz: 2 buzz: 2 fizzbuzz: 0 lucky: 1 integer: 5")
  })

  it("should produce the expected output given numbers 11-20", () => {
    const numbers = [11, 12, 13, 14, 15, 16, 17, 18, 19, 20]
    const output = app(numbers)

    output.should.equal("11 fizz lucky 14 fizzbuzz 16 17 fizz 19 buzz fizz: 2 buzz: 1 fizzbuzz: 1 lucky: 1 integer: 5")
  })

  it("should produce the expected output given numbers 1-20", () => {
    const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20]
    const output = app(numbers)

    output.should.equal("1 2 lucky 4 buzz fizz 7 8 fizz buzz 11 fizz lucky 14 fizzbuzz 16 17 fizz 19 buzz fizz: 4 buzz: 3 fizzbuzz: 1 lucky: 2 integer: 10")
  })
})