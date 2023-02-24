import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import agent from "../../app/api/agenct";
import { Basket } from "../../app/models/basket";
import { getCookie } from "../../app/util/util";

interface BasketState {
    basket: Basket | null;
    status: string;
}

const initialState: BasketState = {
    basket: null,
    status: 'idle',
}

export const addBasketItemAsync = createAsyncThunk<Basket, {productKey : string, quantity?: number}>(
    'basket/addBasketItemAsync',
    async({productKey, quantity = 1}, thunkApi) => {
        try {
            return await agent.Basket.addItem(productKey, quantity);
        } catch(error: any) {
            thunkApi.rejectWithValue({error: error.data})
        }
    }
)

export const removeBasketItemAsync = createAsyncThunk<void,
    {productKey : string, quantity?: number, name?: string}>(
    'basket/removeBasketItemAsync',
    async({productKey, quantity = 1}, thunkApi) => {
        try {
            return await agent.Basket.removeItem(productKey, quantity);
        } catch(error : any) {
            thunkApi.rejectWithValue({error: error.data})
        }
    }
)

export const fetchBasketAsync = createAsyncThunk<Basket>(
    'basket/fetchBasketAsync',
    async(_, thunkAPI) =>
    {
        try {
            return await agent.Basket.get();
        }
        catch(error : any) {
            thunkAPI.rejectWithValue({error: error.data})
        }
    },
    {
        condition: () => {
            if (!getCookie('userKey')) return false;
        }
    }

)

export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload
        },
    },
    extraReducers: (builder => {
        builder.addCase(addBasketItemAsync.pending, (state, action) => {
            console.log(action);
            state.status = 'pendingAddItem' + action.meta.arg.productKey;
        });
        builder.addCase(removeBasketItemAsync.pending, (state, action) => {
            console.log(action);
            state.status = 'pendingRemoveItem' + action.meta.arg.productKey + action.meta.arg.name;
        });
        builder.addCase(removeBasketItemAsync.fulfilled, (state, action) =>
        {
            const {productKey, quantity} = action.meta.arg;
            const itemIndex = state.basket?.items.findIndex(i => i.productKey === productKey);
            if (itemIndex === -1 || itemIndex === undefined) return;
            state.basket!.items[itemIndex].quantity -= quantity!;
            if (state.basket?.items[itemIndex].quantity === 0)
                state.basket.items.splice(itemIndex, 1);
            state.status = 'idle';
        });
        builder.addCase(removeBasketItemAsync.rejected, (state) =>
        {
            state.status = 'idle';
        });
        builder.addMatcher(isAnyOf(addBasketItemAsync.fulfilled, fetchBasketAsync.fulfilled), (state, action) =>
        {
            state.basket = action.payload;
            state.status = 'idle';
        });
        builder.addMatcher(isAnyOf(addBasketItemAsync.rejected, fetchBasketAsync.rejected), (state) =>
        {
            state.status = 'idle';
        });
    })
})

export const {setBasket} = basketSlice.actions;