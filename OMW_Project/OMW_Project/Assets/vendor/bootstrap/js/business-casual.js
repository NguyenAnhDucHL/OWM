// ************************************************
// Shopping Cart API
// ************************************************

var shoppingCart = (function () {
    // =============================
    // Private methods and propeties
    // =============================
    cart = [];

    // Constructor
    function Item(Id, price, count, name) {
        this.name = name;
        this.Id = Id;
        this.price = price;
        this.count = count;
    }

    // Save cart
    function saveCart() {
        sessionStorage.setItem('shoppingCart', JSON.stringify(cart));
    }

    // Load cart
    function loadCart() {
        cart = JSON.parse(sessionStorage.getItem('shoppingCart'));
    }
    if (sessionStorage.getItem("shoppingCart") != null) {
        loadCart();
    }


    // =============================
    // Public methods and propeties
    // =============================
    var obj = {};

    // Add to cart
    obj.addItemToCart = function (Id, price, count,name) {
        for (var item in cart) {
            if (cart[item].Id === Id) {
                cart[item].count++;
                saveCart();
                return;
            }
        }
        var item = new Item(Id, price, count, name);
        cart.push(item);
        saveCart();
    }
    // Set count from item
    obj.setCountForItem = function (Id, count) {
        for (var i in cart) {
            if (cart[i].Id === Id) {
                cart[i].count = count;
                break;
            }
        }
    };
    // Remove item from cart
    obj.removeItemFromCart = function (Id) {
        for (var item in cart) {
            if (cart[item].Id === Id) {
                cart[item].count--;
                if (cart[item].count === 0) {
                    cart.splice(item, 1);
                }
                break;
            }
        }
        saveCart();
    }

    // Remove all items from cart
    obj.removeItemFromCartAll = function (Id) {
        for (var item in cart) {
            if (cart[item].Id === Id) {
                cart.splice(item, 1);
                break;
            }
        }
        saveCart();
    }

    // Clear cart
    obj.clearCart = function () {
        cart = [];
        saveCart();
    }

    // Count cart 
    obj.totalCount = function () {
        var totalCount = 0;
        for (var item in cart) {
            totalCount += cart[item].count;
        }
        return totalCount;
    }

    // Total cart
    obj.totalCart = function () {
        var totalCart = 0;
        for (var item in cart) {
            totalCart += cart[item].price * cart[item].count;
        }
        return formatMoney(totalCart.toFixed(0));
    }

    // List cart
    obj.listCart = function () {
        var cartCopy = [];
        for (i in cart) {
            item = cart[i];
            itemCopy = {};
            for (p in item) {
                itemCopy[p] = item[p];

            }
            itemCopy.total = Number(item.price * item.count).toFixed(0);
            cartCopy.push(itemCopy)
        }
        return cartCopy;
    }

    // cart : Array
    // Item : Object/Class
    // addItemToCart : Function
    // removeItemFromCart : Function
    // removeItemFromCartAll : Function
    // clearCart : Function
    // countCart : Function
    // totalCart : Function
    // listCart : Function
    // saveCart : Function
    // loadCart : Function
    return obj;
})();


// *****************************************
// Triggers / Events
// ***************************************** 
// convert
function formatMoney(amount, decimalCount = 0, decimal = ".", thousands = ",") {
	try {
		decimalCount = Math.abs(decimalCount);
		decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

		const negativeSign = amount < 0 ? "-" : "";

		let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
		let j = (i.length > 3) ? i.length % 3 : 0;

		return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
	} catch (e) {
		console.log(e)
	}
};



// *****************************************
// Triggers / Events
// ***************************************** 
// Add item
$('.add-to-cart').click(function (event) {
    event.preventDefault();
    var Id = $(this).attr('data-Id');
    var name = $(this).data('name');
    var price = Number($(this).data('price'));
    shoppingCart.addItemToCart(Id, price, 1,name);
    displayCart();
});

// Clear items
$('.clear-cart').click(function () {
    shoppingCart.clearCart();
    displayCart();
});


function displayCart() {
    var cartArray = shoppingCart.listCart();
    var output = "";
    for (var i in cartArray) {
	    debugger
        output += "<tr>"
            + "<td>" + cartArray[i].name + "</td>"
            + "<td>(" + formatMoney(cartArray[i].price) + ")</td>"
            + "<td><div class='input-group'><button class='minus-item input-group-addon btn btn-primary' data-Id='" + cartArray[i].Id + "'>-</button>"
            + "<input type='number' class='item-count form-control' data-Id='" + cartArray[i].Id + "' value='" + cartArray[i].count + "'>"
            + "<button class='plus-item btn btn-primary input-group-addon' data-Id='" + cartArray[i].Id + "'>+</button></div></td>"

            + "<td><div class='input-group'>"
            + "<button class='delete-item btn btn-danger' data-Id='" + cartArray[i].Id + "'>X</button></div></td>"
            //+ " = "
            + "<td>" + formatMoney(cartArray[i].total) + "</td>"
            + "</tr>";
    }
    $('.show-cart').html(output);
    $('.total-cart').html(shoppingCart.totalCart());
    $('.total-count').html(shoppingCart.totalCount());
}

// Delete item button

$('.show-cart').on("click", ".delete-item", function (event) {
    var Id = $(this).attr('data-Id')
    shoppingCart.removeItemFromCartAll(Id);
    displayCart();
})


// -1
$('.show-cart').on("click", ".minus-item", function (event) {
    var Id = $(this).attr('data-Id')
    shoppingCart.removeItemFromCart(Id);
    displayCart();
})
// +1
$('.show-cart').on("click", ".plus-item", function (event) {
    var Id = $(this).attr('data-Id')
    shoppingCart.addItemToCart(Id);
    displayCart();
})

// Item count input
$('.show-cart').on("change", ".item-count", function (event) {
    var Id = $(this).attr('data-Id');
    var count = Number($(this).val());
    shoppingCart.setCountForItem(Id, count);
    displayCart();
});

displayCart();
