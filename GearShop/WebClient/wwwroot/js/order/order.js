﻿const ShowListAddress = () => {
    $('#listAddress').css('display', 'block');
};

const CloseListAddress = () => {
    $('#listAddress').css('display', 'none');
}

const ChangeAddress = () => {
    var selectedRadio = $('input[name="addressId"]:checked');

    if (selectedRadio.length > 0) {
        var fullname = selectedRadio.data('fullname');
        var phone = selectedRadio.data('phone');
        var specific = selectedRadio.data('specific');
        var address = selectedRadio.data('address');

        $('#formFullname').text(fullname);
        $('#formPhone').text(phone);

        if (specific) {
            $('#formAddress').text(specific + ', ' + address);
            $('#ChosenADA').val(specific + ', ' + address);
        } else {
            $('#formAddress').text(address);
            $('#ChosenADA').val(address);
        }

        $('#ChosenFNA').val(fullname);
        $('#ChosenPNA').val(phone);
        $('#listAddress').css('display', 'none');
    } else {
        alert('Please select an address.');
    }
}

const UpdateAddress = (button) => {
    // Get data from the button
    const $button = $(button);
    const fullname = $button.data('fullname');
    const phone = $button.data('phonenum');
    const address = $button.data('address');
    const addressID = $button.data('id');
    const isdefault = $button.data('isdefault');
    const specific = $button.data('specific');
    const $checkbox = $('#defaultAddress');

    // Populate the form fields with the existing data
    $('#idUpdate').val(addressID);
    $('#fullnameUpdate').val(fullname);
    $('#phonenumUpdate').val(phone);
    $('#addressUpdate').val(address);
    $('#specificAddressUpdate').val(specific);

    if (isdefault === true) {
        $checkbox.prop('checked', true).prop('disabled', true); // Disable the checkbox if it's checked
    } else {
        $checkbox.prop('checked', false).prop('disabled', false); // Enable the checkbox if it's not checked
    }

    $('#updateAddressForm').data('addressid', addressID);

    // Show the popup form
    $('#editAddress').css('display', 'block');
    $('#listAddress').css('display', 'none');
}

const CreateAddress = () => {
    $('#createAddress').css('display', 'block');
    $('#listAddress').css('display', 'none');
}

const CloseForm = (type) => {
    $('#listAddress').css('display', 'block');
    if (type == 0) {
        $('#fullName').val('');
        $('#phoneNumber').val('');
        $('#address').val('');
        $('#createAddress').css('display', 'none');
    } else {
        $('#editAddress').css('display', 'none');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    fetchProvinces();
    fetchProvincesUpdate();
});

async function fetchProvinces() {
    try {
        const response = await fetch('/dataAddress/provinces.json');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Provinces JSON data:', data); // Thêm dòng này để kiểm tra dữ liệu JSON


        if (data && data.data && Array.isArray(data.data.data)) {
            const provinces = data.data.data;
            const provincesSelect = document.getElementById('provinces');
            provincesSelect.innerHTML = '<option value="">Select district</option>';
            provinces.forEach(province => {
                const option = document.createElement('option');
                option.value = province.code;
                option.text = province.name_with_type;
                provincesSelect.appendChild(option);
            });
        } else {
            console.error('Unexpected data structure:', data);
        }
    } catch (error) {
        console.error('Error fetching provinces:', error);
    }
}
async function getDistricts(event) {
    const provinceCode = event.target.value;
    if (!provinceCode) return;

    try {
        const response = await fetch('/dataAddress/districts.json');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Districts JSON data:', data); // Thêm dòng này để kiểm tra dữ liệu JSON

        if (data && data.data && Array.isArray(data.data.data)) {
            const districts = data.data.data.filter(district => district.parent_code === provinceCode);
            const districtsSelect = document.getElementById('districts');
            districtsSelect.innerHTML = '<option value="">Select district</option>'; // Clear previous options
            districts.forEach(district => {
                const option = document.createElement('option');
                option.value = district.code;
                option.textContent = district.name_with_type;
                districtsSelect.appendChild(option);
            });
        } else {
            console.error('Unexpected data structure:', data);
        }
    } catch (error) {
        console.error('Error fetching districts:', error);
    }
}
async function getWards(event) {
    const districtCode = event.target.value;
    if (!districtCode) return;

    try {
        const response = await fetch('/dataAddress/wards.json');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Wards JSON data:', data); // Thêm dòng này để kiểm tra dữ liệu JSON

        if (data && data.data && Array.isArray(data.data.data)) {
            const wards = data.data.data.filter(ward => ward.parent_code === districtCode);
            const wardsSelect = document.getElementById('wards');
            wardsSelect.innerHTML = '<option value="">Select ward</option>'; // Clear previous options
            wards.forEach(ward => {
                const option = document.createElement('option');
                option.value = ward.code;
                option.textContent = ward.name_with_type;
                wardsSelect.appendChild(option);
            });
        } else {
            console.error('Unexpected data structure:', data);
        }
    } catch (error) {
        console.error('Error fetching wards:', error);
    }
}

function updateAddressField() {
    const provincesSelect = document.getElementById('provinces');
    const districtsSelect = document.getElementById('districts');
    const wardsSelect = document.getElementById('wards');
    const addressInput = document.getElementById('address');
    const specificAddressInput = document.getElementById("specificAddress");

    // Lấy giá trị đã chọn từ các dropdown

    const provinceText = provincesSelect.options[provincesSelect.selectedIndex].text;
    const districtText = districtsSelect.options[districtsSelect.selectedIndex].text;
    const wardText = wardsSelect.options[wardsSelect.selectedIndex].text;

    // Cập nhật giá trị vào ô input
    addressInput.value = `${wardText}, ${districtText}, ${provinceText}`;
}

async function fetchProvincesUpdate() {
    try {
        const response = await fetch('/dataAddress/provinces.json');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Provinces JSON data:', data); // Thêm dòng này để kiểm tra dữ liệu JSON


        if (data && data.data && Array.isArray(data.data.data)) {
            const provinces = data.data.data;
            const provincesSelect = document.getElementById('provincesUpdate');
            provincesSelect.innerHTML = '<option value="">Select district</option>';
            provinces.forEach(province => {
                const option = document.createElement('option');
                option.value = province.code;
                option.text = province.name_with_type;
                provincesSelect.appendChild(option);
            });
        } else {
            console.error('Unexpected data structure:', data);
        }
    } catch (error) {
        console.error('Error fetching provinces:', error);
    }
}


async function getDistrictsUpdate(event) {
    const provinceCode = event.target.value;
    if (!provinceCode) return;

    try {
        const response = await fetch('/dataAddress/districts.json');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Districts JSON data:', data); // Thêm dòng này để kiểm tra dữ liệu JSON

        if (data && data.data && Array.isArray(data.data.data)) {
            const districts = data.data.data.filter(district => district.parent_code === provinceCode);
            const districtsSelect = document.getElementById('districtsUpdate');
            districtsSelect.innerHTML = '<option value="">Select district</option>'; // Clear previous options
            districts.forEach(district => {
                const option = document.createElement('option');
                option.value = district.code;
                option.textContent = district.name_with_type;
                districtsSelect.appendChild(option);
            });
        } else {
            console.error('Unexpected data structure:', data);
        }
    } catch (error) {
        console.error('Error fetching districts:', error);
    }
}

async function getWardsUpdate(event) {
    const districtCode = event.target.value;
    if (!districtCode) return;

    try {
        const response = await fetch('/dataAddress/wards.json');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        console.log('Wards JSON data:', data); // Thêm dòng này để kiểm tra dữ liệu JSON

        if (data && data.data && Array.isArray(data.data.data)) {
            const wards = data.data.data.filter(ward => ward.parent_code === districtCode);
            const wardsSelect = document.getElementById('wardsUpdate');
            wardsSelect.innerHTML = '<option value="">Select ward</option>'; // Clear previous options
            wards.forEach(ward => {
                const option = document.createElement('option');
                option.value = ward.code;
                option.textContent = ward.name_with_type;
                wardsSelect.appendChild(option);
            });
        } else {
            console.error('Unexpected data structure:', data);
        }
    } catch (error) {
        console.error('Error fetching wards:', error);
    }
}

function AddressFieldEdit() {
    const provincesSelect = document.getElementById('provincesUpdate');
    const districtsSelect = document.getElementById('districtsUpdate');
    const wardsSelect = document.getElementById('wardsUpdate');
    const addressInput = document.getElementById('addressUpdate');

    // Lấy giá trị đã chọn từ các dropdown

    const provinceText = provincesSelect.options[provincesSelect.selectedIndex].text;
    const districtText = districtsSelect.options[districtsSelect.selectedIndex].text;
    const wardText = wardsSelect.options[wardsSelect.selectedIndex].text;

    // Cập nhật giá trị vào ô input
    addressInput.value = `${wardText}, ${districtText}, ${provinceText}`;
}

$('.add_address_form').on('submit', function (event) {
    event.preventDefault(); // Prevent the default form submission
    var fullname = getValueById('fullName');
    var phone = getValueById('phoneNumber');
    var address = getValueById('address');
    var specificAddress = getValueById('specificAddress');
    var noError = true;


    if (!fullname) {
        showError('fullName', 'Please enter your full name');
        noError = false;
    } else if (fullname.length > 100) {
        showError('fullName', 'You full name must be less than 100 characters!');
        noError = false;
    } else {
        hideError('fullName');
    }


    if (!phone) {
        showError('phoneNumber', 'Please enter your phone number!');
        noError = false;
    } else if (!isValidPhoneNumber(phone)) {
        showError('phoneNumber', 'Invalid phone number!');
        noError = false;
    } else {
        hideError('phoneNumber');
    }


    if (!address) {
        showError('address', 'Please enter address');
        noError = false;
    }else {
        hideError('address');
    }


    if (!specificAddress) {
        showError('specificAddress', 'Please enter specific address');
        noError = false;
    }else {
        hideError('specificAddress');
    }
    if (noError) {
        var form = $(this);
        var url = form.attr('action');
        var formData = form.serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            success: function (response) {
                location.reload();
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                // Optionally, handle errors here
            }
        });
    }
});

$('.update_address_form').on('submit', function (event) {
    event.preventDefault(); // Prevent the default form submission
    var fullname = getValueById('fullnameUpdate');
    var phone = getValueById('phonenumUpdate');
    var address = getValueById('addressUpdate');
    var specificAddress = getValueById('specificAddressUpdate');
    var noError = true;


    if (!fullname) {
        showError('fullnameUpdate', 'Please enter your full name');
        noError = false;
    } else if (fullname.length > 100) {
        showError('fullnameUpdate', 'You full name must be less than 100 characters!');
        noError = false;
    } else {
        hideError('fullnameUpdate');
    }


    if (!phone) {
        showError('phonenumUpdate', 'Please enter your phone number!');
        noError = false;
    } else if (!isValidPhoneNumber(phone)) {
        showError('phonenumUpdate', 'Invalid phone number!');
        noError = false;
    } else {
        hideError('phonenumUpdate');
    }


    if (!address) {
        showError('addressUpdate', 'Please enter address');
        noError = false;
    } else {
        hideError('addressUpdate');
    }


    if (!specificAddress) {
        showError('specificAddressUpdate', 'Please enter specific address');
        noError = false;
    } else {
        hideError('specificAddressUpdate');
    }
    if (noError) {
        var form = $(this);
        var url = form.attr('action');
        var formData = form.serialize();

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            success: function () {
                // Reload the page upon successful submission
                location.reload();
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
                // Optionally, handle errors here
            }
        });
    }
   
})
    ; function getValueById(id) {
    return $('#' + id).val();
}

// Hiển thị thông báo lỗi cho một trường
function showError(id, message) {
    $('#' + id).next('p').text(message);
}

// Ẩn thông báo lỗi của một trường
function hideError(id) {
    $('#' + id).next('p').text('');
}
