$(function () {

    $(document).on("click", ".add-product-basket", function () {

        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `home/addproducttobasket?id=${id}`,
            type: 'post',
            success: function (response) {

               
                $(".circle-count-basket").text(response.count);

            }
        })

    })

})


//remove basket

$(function () {

    $(document).on("click", ".delete-product-basket", function () {

        let id = parseInt($(this).attr("data-id"));
        console.log(id);

        $.ajax({
            url: `cart/DeleteProductFromBasket?id=${id}`,
            type: 'post',
            success: function (response) {

                $(".cart-total").text(`${response.total}`);
                $(".circle-count-basket").text(`${response.count}`);
                $(`[data-id = ${id}]`).parent().parent().remove();

            }
        });

    });

});
