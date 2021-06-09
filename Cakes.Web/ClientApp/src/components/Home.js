import React from 'react'
import { useHistory } from "react-router-dom";
import useAxios from 'axios-hooks'
import {Card, CardBody, CardColumns, CardImg, CardTitle, Spinner} from "reactstrap";

export function Home() {
    const[{data, loading, error}] = useAxios(`${process.env.REACT_APP_API_URL}/cakes`);
    const history = useHistory();
    
    if (loading) return <Spinner type="grow" color="primary" />
    
    const viewClick = (id) => {
        history.push(`/cake/${id}`)
    }
    
    const renderCake = (cake) => {
      return (
        <Card className="cursor-pointer" onClick={() => viewClick(cake.id)}>
            <CardImg top width="100%" src={cake.imageUrl} alt="Cake image" />
            <CardBody>
                <CardTitle tag="h5">{cake.name}</CardTitle>
            </CardBody>
        </Card>
      )  
    };
    return (
        <CardColumns>
            {data.map(renderCake)}
        </CardColumns>
    );
}
