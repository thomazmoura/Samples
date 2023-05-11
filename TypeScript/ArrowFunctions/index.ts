var timesInstantiated = 0;
var timesTheIdWasChecked = 0;
export class SampleClassThatAddsOneEveryTimeItsInstantiated {

  constructor(public id: number) {
    timesInstantiated++;
  }

  getId() {
    timesTheIdWasChecked++;
    return this.id;
  }
}

let result = [1,2,3,4,5]
  .map(index => new SampleClassThatAddsOneEveryTimeItsInstantiated(index))
  .filter(sample => sample.getId() > 3)[0];

console.log(`Times instantiated: ${timesInstantiated}`);
console.log(`Times the id was checked: ${timesTheIdWasChecked}`);

