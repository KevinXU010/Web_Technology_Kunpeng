import Card from "./CardV3"
import itemData from "../assets/itemData.json"

const CardList = ({ }) => {

    console.log("cardData: " + itemData);
    return (
        <div className="row">
            {itemData.map((obj) => (
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

export default CardList