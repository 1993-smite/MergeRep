//import * as BasketMdl from '../Components/ts/basket';

let toys: Array<Toy> = [];
for (let i = 0; i < 10; i++) {
    toys.push(new Toy(i,`toy ${i}`, i+10));
}

let basket = new Basket<Toy>(1, toys);
//basket.AddItem(new Toy(11, 'temp', 200));

let bskt = BasketFactory.BasketWithPrice(new Basket<Toy>(2, toys));
let bsktWithBudget = BasketFactory.BasketByBudget(bskt, 200);

bskt.AddItem(new Toy(20, "test", 100));


bsktWithBudget.AddItem(new Toy(21, "test", 140));

console.log('basket', basket, bskt, bsktWithBudget);

console.log(bskt.GetPrice());