import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";

axios.defaults.baseURL = 'https://localhost:7158/api/';
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(response => {
    return response
}, (error: AxiosError<any>) => {
    const {data, status} = error.response!;
    switch(status) {
        case 400:
            if (data.errors) {
                const modelStateErrors: string[] = [];
                for(const key in data.errors) { 
                    modelStateErrors.push(data.errors[key])
                }
                throw modelStateErrors.flat();
            }
            toast.error(data.title);
            break;
        case 401:
            toast.error(data.title);
            break;
        case 500:
            toast.error(data.title);
            break;
        default:
            toast.error(data.title);
            break;
    }
    return Promise.reject(error.response);
})

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}

const Catalog = {
    list: () => requests.get('product'),
    details: (id: string) => requests.get(`product/${id}`)
}

const Basket = {
    get: () => requests.get('basket'),
    addItem: (productKey: string , quantity = 1) => requests.post(`basket/AddToCart?productKey=${productKey}&quantity=${quantity}`, {}),
    addItems: (productKey: string , quantity: number) => requests.post(`basket?productKey=${productKey}&quantity=${quantity}`, {}),
    removeItem: (productKey: string , quantity: number) => requests.delete(`basket?productKey=${productKey}&quantity=${quantity}`),

}

const agent = {
    Catalog,
    Basket
}

export default agent;