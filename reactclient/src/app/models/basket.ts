export interface BasketItem {
    productKey: string,
    name: string,
    type: string;
    brand: string;
    quantity: number,
    price: number,
}

export interface Basket {
    userKey: string;
    basketKey: string;
    userId: string;
    items: BasketItem[];
}