var Extensions = /** @class */ (function () {
    function Extensions() {
    }
    Extensions.swap = function (a, b) {
        var temp = a;
        a = b;
        b = temp;
    };
    Extensions.createArray = function (arr) {
        var nArr = new Array();
        for (var _i = 0, arr_1 = arr; _i < arr_1.length; _i++) {
            var itm = arr_1[_i];
            nArr.push(itm);
        }
        return nArr;
    };
    Extensions.reducer = function (accumulator, currentValue) { return accumulator + currentValue; };
    return Extensions;
}());
var Toy = /** @class */ (function () {
    function Toy(id, name, price) {
        if (id === void 0) { id = 0; }
        if (name === void 0) { name = ""; }
        if (price === void 0) { price = 0; }
        this.Id = id;
        this.Name = name;
        this.Price = price;
    }
    return Toy;
}());
var Basket = /** @class */ (function () {
    function Basket(id, items) {
        this.Items = null;
        this.Id = id;
        this.Items = Extensions.createArray(items);
    }
    Basket.prototype.AddItem = function (item) {
        this.Items.push(item);
    };
    Basket.prototype.RemoveItem = function (item) {
        this.Items = this.Items.filter(function (x) { return x.Id === item.Id; });
    };
    return Basket;
}());
var BasketFactory = /** @class */ (function () {
    function BasketFactory() {
    }
    BasketFactory.BasketWithPrice = function (basket) {
        var basketWithPrice = Object.create(basket);
        basketWithPrice.GetPrice = function () {
            return this.Items.reduce(function (accumulator, currentValue) { return accumulator + currentValue.Price; }, 0);
        };
        return basketWithPrice;
    };
    BasketFactory.BasketByBudget = function (basket, budget) {
        var basketByBudget = Object.create(basket);
        basketByBudget.Budget = budget;
        basketByBudget.CheckBudget = function () {
            return this.GetPrice() <= this.Budget;
        };
        var addItm = basketByBudget.AddItem;
        basketByBudget.AddItem = function (item) {
            if (this.GetPrice() + item.Price <= this.Budget) {
                addItm.call(this, item);
            }
        };
        return basketByBudget;
    };
    return BasketFactory;
}());
//# sourceMappingURL=basket.js.map