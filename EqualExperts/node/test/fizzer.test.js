require("chai").should()

const fizzer = require("../src/fizzer")

describe("fizzer", () => {
  it("should return the expected report", () => {

    const numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    const output = fizzer(numbers)

    output.should.equal("1 2 fizz 4 buzz fizz 7 8 fizz buzz")
  })
})
