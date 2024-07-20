var currentDepartemntId = $('#Id').val();

document.addEventListener("DOMContentLoaded", function () {
    loadSubDepartments(null, 0);
    document.getElementById('departmentDropdowns').addEventListener('change', function (event) {
        if (event.target.classList.contains('department-select')) {
            var selectedValue = event.target.value;
            var level = parseInt(event.target.dataset.level);
            AddDropdown(selectedValue, level);
        }
    });
});

function loadSubDepartments(parentId, level) {
    var select = document.createElement('select');
    fetch('/Departments/GetSubDepartments?id=' + (parentId || ''))
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                select.classList.add('form-control', 'department-select', 'department-custome-style');
                select.dataset.level = level;
                var option = document.createElement('option');
                option.value = '';
                option.text = 'Select Parent Department';
                select.appendChild(option);

                data.forEach(department => {
                    if (currentDepartemntId != department.id) {
                        var option = document.createElement('option');
                        option.value = department.id;
                        option.text = department.name;
                        select.appendChild(option);
                    }
                });

                if ($(select).find('option').length > 1) {
                    document.getElementById('departmentDropdowns').appendChild(select);
                    if (parents != undefined && parents.length > 0) {
                        selectFirstMatchingOption(select, level);
                    }
                }
            }
            ClearDepartmentContainer(level, (data.length < 1 || $(select).find('option').length < 2))
        });
}

function ClearDepartmentContainer(level, condition) {
    if (level == 0 && condition) {
        $('#departmentContainerDev').remove();
    }
}

function AddDropdown(selectedValue, level) {
    var dropdowns = document.querySelectorAll('.department-select');
    dropdowns.forEach(function (dropdown) {
        if (parseInt(dropdown.dataset.level) > level) {
            dropdown.parentElement.removeChild(dropdown);
        }
    });

    dropdowns = document.querySelectorAll('.department-select');
    if (selectedValue) {
        loadSubDepartments(selectedValue, level + 1);
        document.getElementById('ParentDepartmentId').value = selectedValue;
    } else {
        document.getElementById('ParentDepartmentId').value = dropdowns.length > 1 ? dropdowns[dropdowns.length - 2].value : "";
    }
    console.log('ParentDepartmentId value is:', document.getElementById('ParentDepartmentId').value);
}

function selectFirstMatchingOption(select, level) {
    let selectedValue = null;

    $(select).find('option').each(function () {
        const optionValue = parseInt($(this).val());
        if (parents.some(parent => parent.id === optionValue)) {
            $(this).prop('selected', true);
            selectedValue = optionValue;
            return false; // break the loop
        }
    });

    if (selectedValue == null) {
        $(select).val('');
    } else {
        AddDropdown(selectedValue, level);
    }
}