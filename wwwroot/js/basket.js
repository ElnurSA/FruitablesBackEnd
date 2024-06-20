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
//$(document).ready(function () {
//    $('.category-link').click(function (e) {
//        e.preventDefault();
//        var categoryId = $(this).data('id');
  
//        $('.category-link').removeClass('active');

       
//        $(this).addClass('active');

//        filterProductsByCategory(categoryId);
//    });

//    function filterProductsByCategory(categoryId) {
//        $('.category-item').each(function () {
//            var productCategoryId = $(this).data('id');
//            if (productCategoryId == categoryId || categoryId == 'all') {
//                $(this).show();
//            } else {
//                $(this).hide();
//            }
//        });
//    }

    
//    filterProductsByCategory('all');
//    $('.category-link').first().addClass('active');
//});

$(document).ready(function () {
    // Handle category click
    $('.category-link').on('click', function (e) {
        e.preventDefault();
        var categoryId = $(this).data('id');

        // Remove active class from all category links and add to the clicked one
        $('.category-link').removeClass('active');
        $(this).addClass('active');

        // Show all products if "All Products" is clicked
        if (categoryId === 'all') {
            $('.category-item').show();
        } else {
            // Hide all products and show only those with the selected category
            $('.category-item').hide();
            $('.category-item[data-id="' + categoryId + '"]').show();
        }
    });

    // Handle "All Products" click
    $('#all-products').on('click', function (e) {
        e.preventDefault();
        // Remove active class from all category links and add to the "All Products" link
        $('.category-link').removeClass('active');
        $(this).addClass('active');

        // Show all products
        $('.category-item').show();
    });
});
