import React, { useState } from 'react'
import Card from "./Card"

const CardList = () => {


    const [cardData, setState] = useState([ ]);
    React.useEffect(() => {
        fetch('http://localhost:5156/api/ItemsWebAPI')
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err)
            });

    }, [ ])

    return (
        <div className="row">
            {cardData.map((obj) => (
                <Card
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemDescription={obj.itemDescription}
                    itemCost={obj.itemCost}
                    itemImage={obj.itemImage}
                />
            )
            )
            }
        </div>

    )
}

export default CardList;
