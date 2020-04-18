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
}

/* ************************************************************************************************************ */

interface IToy {
    readonly Id: number;
    Name: string;
}

class Toy implements IToy {
    public Id: number;
    public Name: string;
    public Count: number;

    constructor(id: number = 0, name: string = "", count: number = 0) {
        this.Id = id;
        this.Name = name;
        this.Count = count;
    }

    addCount(count: number) {
        this.Count += count;
    }
}

class ToyPrice extends Toy {
    public Price: number;

    constructor(id: number = 0, name: string = "", count: number = 1, price: number = 0) {
        super(id, name, count)
        this.Price = price;
    }
}

/* ************************************************************************************************************ */

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

    public static BasketWithPrice<T extends ToyPrice, TBasket extends Basket<T>>(basket: TBasket) {
        let basketWithPrice = Object.create(basket);
        basketWithPrice.GetPrice = function():number {
            return this.Items.reduce((accumulator, currentValue) => accumulator + currentValue.Price * currentValue.Count, 0);
        }
        return basketWithPrice;
    }

    /**
     * 
     * @param basket
     * @param maxCount
     */
    public static BasketWithMaxCount<T extends Toy, TBasket extends Basket<T>>(basket: TBasket, maxCount: number) {
        let basketWithMaxCount = Object.create(basket);
        basketWithMaxCount.MaxCount = maxCount;
        basketWithMaxCount.GetCount = function (): number {
            return this.Items.reduce((accumulator, currentValue) => accumulator + currentValue.Count, 0);
        }
        let addItm: Function = basketWithMaxCount.AddItem;
        basketWithMaxCount.AddItem = function (item: T) {
            if (this.GetCount() + item.Count <= this.MaxCount) {
                addItm.call(this, item);
            }
        }
        return basketWithMaxCount;
    }

    public static BasketByBudget<T extends ToyPrice, TBasket extends Basket<T>>(basket: TBasket, budget: number) {
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