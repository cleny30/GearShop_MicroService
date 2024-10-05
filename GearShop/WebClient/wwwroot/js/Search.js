const Search = (element) => {
    const pattern = $(element).val();
    const productList = $('#product-list');
    const noResults = $('#no-results');

    if (pattern.length >= 3) {
        $.ajax({
            url: '/Shop/Search',
            type: "GET",
            data: { pattern: pattern },
            success: function (data) {
                productList.empty(); // Clear the product list
                noResults.addClass('d-none'); // Hide "no results" message initially

                if (data && data.result.length > 0) {
                    productList.removeClass('d-none'); // Show product list

                    data.result.forEach(product => {
                        const url = '/ProductDetail?proId=' + product.proId;
                        const productDiv = $('<div class="product" onclick="location.href=\'' + url + '\'">');
                        productDiv.append('<img src="' + product.proImg[0] + '" alt="" style="width:100px;">');
                        const pDetailsDiv = $('<div class="p-details">');
                        pDetailsDiv.append('<h2>' + product.proName + '</h2>');

                        if (product.discount > 0) {
                            const priceDiscount = product.proPrice - (product.proPrice * product.discount) / 100;
                            pDetailsDiv.append('<h3>$' + priceDiscount.toFixed(2) + ' <span class="text-muted ml-2"><del>$' + product.proPrice.toFixed(2) + '</del></span></h3>');
                        } else {
                            pDetailsDiv.append('<h3>$' + product.proPrice.toFixed(2) + '</h3>');
                        }

                        productDiv.append(pDetailsDiv);
                        productList.append(productDiv);
                    });
                } else {
                    productList.addClass('d-none');
                }
            },
            error: function () {
                productList.addClass('d-none');
            }
        });
    } else {
        productList.addClass('d-none'); // Hide product list when pattern < 3 characters
        noResults.addClass('d-none'); // Hide "no results" message
    }
}
const Clear = (element) => {
    const searchBox = $('#search-item');
    const productList = $('#product-list');

    if (!searchBox.is(':focus') && !productList.is(':hover')) {
        productList.addClass('d-none').empty();
    }
}

$('#search-item').on('blur', function () {
    setTimeout(function () {
        Clear();
    }, 200);
});