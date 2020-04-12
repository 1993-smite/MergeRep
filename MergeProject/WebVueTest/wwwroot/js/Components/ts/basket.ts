class Extensions {
    static swap<T>(a: T, b: T) {
        const temp = a;
        a = b;
        b = temp;
    }

    static reducer = (accumulator, currentValue) => accumulator + currentValue;

    static createArray<T>(arr: Array<T>): Array<T> {
        let nArr = new Array<T>();
        for (let itm of arr) {
            nArr.push(itm);
        }
        return nArr;
    }

    //static copy<T>(obj: T) {
    //    return {
    //        ...obj
    //    }
    //}
}

interface IToy {
    readonly Id: number;
    Name: string;
}

class Toy implements IToy {
    public Id: number;
    public Name: string;
    public Price: number;

    constructor(id: number = 0, name: string = "", price: number = 0) {
        this.Id = id;
        this.Name = name;
        this.Price = price;
    }
}

class Basket<T extends IToy> {
    private Id: number;
    public Items: Array<T> = null;

    constructor(id: number, items: Array<T>) {
        this.Id = id;
        this.Items = Extensions.createArray(items);
    }

    AddItem(item: T) {
        this.Items.push(item);
    }

    RemoveItem(item: T) {
        this.Items = this.Items.filter(x => x.Id === item.Id);
    }
}

class BasketFactory {

    public static BasketWithPrice<T extends Toy, TBasket extends Basket<T>>(basket: TBasket) {
        let basketWithPrice = Object.create(basket);
        basketWithPrice.GetPrice = function():number {
            return this.Items.reduce((accumulator, currentValue) => accumulator + currentValue.Price, 0);
        }
        return basketWithPrice;
    }

    public static BasketByBudget<T extends Toy, TBasket extends Basket<T>>(basket: TBasket, budget: number) {
        let basketByBudget = Object.create(basket);
        basketByBudget.Budget = budget;
        basketByBudget.CheckBudget = function (): boolean {
            return this.GetPrice() <= this.Budget;
        }
        let addItm: Function = basketByBudget.AddItem; 
        basketByBudget.AddItem = function (item: T) {
            if (this.GetPrice() + item.Price <= this.Budget) {
                addItm.call(this, item);
            }
        }
        return basketByBudget;
    }
}