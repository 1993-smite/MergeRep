//import * as BasketMdl from '../Components/ts/basket';
var toys = [];
for (var i = 0; i < 10; i++) {
    toys.push(new ToyPrice(i, "toy " + i, i + 10));
}
var basket = new Basket(1, toys);
//basket.AddItem(new Toy(11, 'temp', 200));
var bskt = BasketFactory.BasketWithPrice(new Basket(2, toys));
var bsktWithBudget = BasketFactory.BasketByBudget(bskt, 200);
bskt.AddItem(new Toy(20, "test", 100));
bsktWithBudget.AddItem(new Toy(21, "test", 140));
console.log('basket', basket, bskt, bsktWithBudget);
console.log(bskt.GetPrice());
//# sourceMappingURL=test-bskt.js.map