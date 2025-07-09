$(function () {
    loadTop10Product();
});

function loadTop10Product() {
    $.ajax({
        url: window.API_URL + `/Shop/TopSold`,
        type: 'Get',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (products) {
            console.log(products);

            $.each(products, function (index, product) {

                let statusText;
                switch (product.status) {
                    case 0:
                        statusText = '<div class="block-pending">Ngừng bán</div>';
                        break;
                    case 1:
                        statusText = '<div class="block-available">Đang bán</div>';
                        break;
                    case 2:
                        statusText = '<div class="block-not-available">Hết hàng</div>';
                        break;
                    default:
                        statusText = "---";
                }

                // xử lý tên quá dài
                let nameText = product.name;
                let listnametext = nameText.split(' ');
                let shortcutName = product.name;
                if (listnametext.length > 4) {
                    shortcutName = listnametext.slice(0, 4).join(' ') + ' ...'
                }
                let price = `${product.minPrice.toLocaleString('vi-VN')} ~ ${product.maxPrice.toLocaleString('vi-VN')}`
                let stock = product.soldAmount;
                let image = `${product.folderImage}/${product.imageView}`;
                $("#product-list").append(
                    `<tr class="antraz-table-list">
                        <th class="antraz-table-item" style="display:flex">
                            <div class="image no-bg">
                                <img style="object-fit: contain; width: 100%;" src="/admin/imgs/product/${image}" alt="">
                            </div>
                            
                            <div class="name">
                                <a class="body-title-2 name-shortcut">${shortcutName}</a>
                            </div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">$${price}</div>
                        </th>
                        <th class="antraz-table-item">
                            <div class="body-text">${stock}</div>
                        </th>
                        <th class="antraz-table-item">
                            ${statusText}
                        </th>
                    </tr>`
                )
            })

        },
        error: function (xhr, status, error) {
            console.error("Lỗi: ", error);
        }
    });

   
}
