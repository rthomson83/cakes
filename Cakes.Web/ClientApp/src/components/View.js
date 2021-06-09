import React from 'react';
import { useParams } from "react-router-dom";
import {Card, CardBody, CardImg, CardSubtitle, CardText, CardTitle, Col, Row, Spinner} from "reactstrap";
import useAxios from "axios-hooks";

export function View() {
    const params = useParams();
    const[{data: cake, loading, error}] = useAxios(`${process.env.REACT_APP_API_URL}/cakes/${params.id}`);
    
    if (loading) return <Spinner type="grow" color="primary" />
    
    return (
        <Row>
            <Col md={{size: 8, offset: 2}}>
                <Card>
                    <CardImg top src={cake.imageUrl} alt="Cake image" />
                    <CardBody>
                        <CardTitle tag="h5">{cake.name}</CardTitle>
                        <CardText>{cake.comment}</CardText>
                        <CardSubtitle tag="h6">Yum Factor</CardSubtitle>
                        <CardText >{cake.yumFactor}</CardText>
                    </CardBody>
                </Card>
            </Col>
        </Row>
    );
}
