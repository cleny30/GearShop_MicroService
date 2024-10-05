const Search = (element) => {
    const inputValue = $(element).val(); 
    const pattern = inputValue.replace(/\s+/g, '');
    const productList = $('#product-list');
    const noResults = $('#no-results');

    if (pattern.length >= 3) {
        $.ajax({
            url: '/Search/SearchProductByName',
            type: "GET",
            data: { pattern: inputValue },
            success: function (data) {
                productList.empty(); // Clear the product list
                noResults.addClass('d-none'); // Hide "no results" message initially

                if (data && data.result.length > 0) {
                    productList.removeClass('d-none'); // Show product list
                    const availableProducts = [];
                    const outOfStockProducts = [];
                    data.result.forEach(product => {
                        if (product.isAvailable == true) {
                          
                            const productDiv = $('<div class="product">');
                            productDiv.append('<img src="' + product.proImg[0] + '" alt="" style="width:100px;">');
                            const pDetailsDiv = $('<div class="p-details">');
                            pDetailsDiv.append('<h2>' + product.proName + '</h2>');

                            if (product.proQuan === 0) {
                                pDetailsDiv.append('<div class="out-of-stock" style="color: red;">Out of Stock</div>');
                                outOfStockProducts.push(productDiv.append(pDetailsDiv));
                            } else
                                if (product.discount > 0) {
                                    const priceDiscount = product.proPrice - (product.proPrice * product.discount) / 100;
                                    pDetailsDiv.append('<h3>$' + priceDiscount.toFixed(2) + ' <span class="text-muted ml-2"><del>$' + product.proPrice.toFixed(2) + '</del></span></h3>');
                                } else {
                                    pDetailsDiv.append('<h3>$' + product.proPrice.toFixed(2) + '</h3>');
                                }
                            availableProducts.push(productDiv.append(pDetailsDiv));
                            const allProducts = availableProducts.concat(outOfStockProducts);
                            allProducts.forEach(item => productList.append(item));
                          
                        }

                       
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

function disableEnter(event) {
    // Ngăn không cho nhấn Enter
    if (event.key === 'Enter') {
        event.preventDefault(); // Ngăn chặn hành động mặc định
        return false; // Trả về false để ngăn gửi biểu mẫu
    }
    return true; // Cho phép các phím khác
}

function updateIcon(input) {
    const icon = document.getElementById('icon');

    
    if (input.value.trim() === '') {
        icon.classList.remove('fa-undo'); 
        icon.classList.add('fa-search'); 
    } else {
        icon.classList.remove('fa-search'); 
        icon.classList.add('fa-undo'); 
    }
}

function resetSearch() {
    const searchBox = document.getElementById('search-item');
    searchBox.value = ''; 
    updateIcon(searchBox); 
}