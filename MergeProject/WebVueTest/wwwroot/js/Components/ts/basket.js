var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
    function Toy(id, name, count) {
        if (id === void 0) { id = 0; }
        if (name === void 0) { name = ""; }
        if (count === void 0) { count = 0; }
        this.Id = id;
        this.Name = name;
        this.Count = count;
    }
    Toy.prototype.addCount = function (count) {
        this.Count += count;
    };
    return Toy;
}());
var ToyPrice = /** @class */ (function (_super) {
    __extends(ToyPrice, _super);
    function ToyPrice(id, name, count, price) {
        if (id === void 0) { id = 0; }
        if (name === void 0) { name = ""; }
        if (count === void 0) { count = 1; }
        if (price === void 0) { price = 0; }
        var _this = _super.call(this, id, name, count) || this;
        _this.Price = price;
        return _this;
    }
    return ToyPrice;
}(Toy));
/* ************************************************************************************************************ */
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
            return this.Items.reduce(function (accumulator, currentValue) { return accumulator + currentValue.Price * currentValue.Count; }, 0);
        };
        return basketWithPrice;
    };
    /**
     *
     * @param basket
     * @param maxCount
     */
    BasketFactory.BasketWithMaxCount = function (basket, maxCount) {
        var basketWithMaxCount = Object.create(basket);
        basketWithMaxCount.MaxCount = maxCount;
        basketWithMaxCount.GetCount = function () {
            return this.Items.reduce(function (accumulator, currentValue) { return accumulator + currentValue.Count; }, 0);
        };
        var addItm = basketWithMaxCount.AddItem;
        basketWithMaxCount.AddItem = function (item) {
            if (this.GetCount() + item.Count <= this.MaxCount) {
                addItm.call(this, item);
            }
        };
        return basketWithMaxCount;
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