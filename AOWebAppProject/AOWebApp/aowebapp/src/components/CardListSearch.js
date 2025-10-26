import React, { useState } from 'react'
import Card from "./CardV3"

function CardListSearch() { 


    const [cardData, setState] = useState([]);
    const [quary, setQuery] = useState(' ');

    React.useEffect(() => {
        console.log("useeffect");
        fetch(`http://localhost:5156/api/ItemsWebAPI?searchText=${quary}`)
            .then(response => response.json())
            .then(data => setState(data))
            .catch(err => {
                console.log(err)
            });

    }, [quary])

    function searchQuery(evt) {
        const value = document.querySelector('[name= searchText]').value;
        alert('value: ' + value);
        setQuery(value);
    }

    return (
        <div id="cardListSearch">
            <div className='col-3'>
                <input type="text " name="searchText" className= "form-control" placeholder= "Type your quary" />
                
            </div>

            <div className="col text- left">
                <button type="button" className="btn btn-primary" onClick={searchQuery}>Search</button>
            </div>
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

        </div>

    )
}

export default CardListSearch;
