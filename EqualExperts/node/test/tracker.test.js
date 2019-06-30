require("chai").should()

const { reset, tracker, getReport } = require("../src/tracker")

describe("tracker - getReport", () => {
  beforeEach(() => {
    reset()
  })

  it("should return 'fizz: 2' when 'fizz' is tracked twice", () => {
    tracker("fizz")
    tracker("fizz")

    const report = getReport()
    report.should.equal("fizz: 2 buzz: 0 fizzbuzz: 0 lucky: 0 integer: 0")
  })

  it("should return 'buzz: 2' when 'buzz' is tracked twice", () => {
    tracker("buzz")
    tracker("buzz")

    const report = getReport()
    report.should.equal("fizz: 0 buzz: 2 fizzbuzz: 0 lucky: 0 integer: 0")
  })

  it("should return 'fizzbuzz: 2' when 'fizzbuzz' is tracked twice", () => {
    tracker("fizzbuzz")
    tracker("fizzbuzz")

    const report = getReport()
    report.should.equal("fizz: 0 buzz: 0 fizzbuzz: 2 lucky: 0 integer: 0")
  })

  it("should return 'lucky: 2' when 'lucky' is tracked twice", () => {
    tracker("lucky")
    tracker("lucky")

    const report = getReport()
    report.should.equal("fizz: 0 buzz: 0 fizzbuzz: 0 lucky: 2 integer: 0")
  })

  it("should return 'integer: 2' when 2 numbers are tracked", () => {
    tracker(2)
    tracker(4)

    const report = getReport()
    report.should.equal("fizz: 0 buzz: 0 fizzbuzz: 0 lucky: 0 integer: 2")
  })
})