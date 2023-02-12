import { LoadingButton } from "@mui/lab";
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, CardMedia, Typography } from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";
import agent from "../../app/api/agenct";
import { Product } from "../../app/models/products";
import { useAppDispatch } from "../../app/store/configureStore";
import { setBasket } from "../basket/basketSlice";

interface Props {
    product: Product;
}

function ProductCard({product}: Props) {
    const [loading, setLoading] = useState(false);

    const dispatch = useAppDispatch();

    function handleAddItem(productKey: string) {
        setLoading(true);
        agent.Basket.addItem(productKey)
            .then(basket => dispatch(setBasket(basket)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }
    
    return (
        <>
            <Card>
                <CardHeader
                    avatar={
                        <Avatar sx={{bgcolor: 'secondary.main'}}>
                            {product.name.charAt(0).toUpperCase()}
                        </Avatar>
                    }
                    title={product.name}
                    titleTypographyProps={{
                        sx: {fontWeight: 'bold', color: 'primary.main'},

                    }}
                />
                <CardMedia
                    sx={{ height: 140, backgroundSize: 'contain', bgcolor: 'primary.light' }}
                    image={product.pictureUrl}
                    title={product.name}
                    component='div'
                />
                <CardContent>
                    <Typography gutterBottom color='secondary' variant="h5">
                        ${(product.price).toFixed(2)}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {product.brand} / {product.type}
                    </Typography>
                </CardContent>
                <CardActions>
                    <LoadingButton
                        loading={loading}
                        onClick={() => handleAddItem(product.productKey)}
                        size="small">Add to cart</LoadingButton>
                    <Button component={Link} to={`/catalog/${product.productId}`} size="small">View</Button>
                </CardActions>
            </Card>
        </>
    )
}

export default ProductCard;