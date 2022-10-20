var url = 'http://localhost:41405';
var depositId = 1;

fetch(`${url}/Data?depositId=${depositId}`)
    .then((response) => response.json())
    .then((data) => {
        $(() => {
            DevExpress.localization.locale("ru");

            var selectBox = $('#reason-rejection').dxSelectBox({
                height: 40,
                dataSource: DevExpress.data.AspNet.createStore({
                    key: 'id',
                    loadUrl: `${url}/Reason`
                }),
                value: data.reasonId,
                valueExpr: 'id',
                displayExpr: 'name',
                deferRendering: false,
                label: 'Причина отклонения',
                labelMode: 'floating',
                searchEnabled: true,
                showClearButton: true
            }).dxValidator({
                validationRules: [{
                    type: 'required',
                    message: ''
                }],
            }).dxSelectBox('instance');

            var dataGrid = $('#parameters').dxDataGrid({
                dataSource: data.parameters,
                keyExpr: 'id',
                editing: {
                    mode: 'batch',
                    allowUpdating: true
                },
                showBorders: true,
                showRowLines: true,
                sorting: {
                    mode: 'none'
                },
                toolbar: {
                    visible: false
                },
                columns: [
                    {
                        dataField: 'name',
                        caption: 'Параметр',
                        allowEditing: false
                    },
                    {
                        dataField: 'agreement',
                        caption: 'На согласование',
                        dataType: 'number',
                        allowEditing: true,
                        validationRules: [{ type: 'required', message: '' }]
                    },
                    {
                        dataField: 'previousData',
                        caption: data.datePrevious,
                        dataType: 'number',
                        allowEditing: false
                    },
                    {
                        dataField: 'difference',
                        caption: '+/-',
                        dataType: 'number',
                        allowEditing: false
                    }
                ],
                onCellPrepared(e) {
                    if (e.rowType === 'data') {
                        if (e.column.name === "name") {
                            switch (e.data.status) {
                                case 1:
                                    e.cellElement.css("color", "green");
                                    break;
                                case 2:
                                    e.cellElement.css("color", "red");
                                    break;
                            }

                            if (e.data.status && e.data.status != 3) {
                                e.cellElement.mouseover(function (arg) {
                                    tooltipInstance.option("contentTemplate", function (contentElement) {
                                        contentElement.html(`${e.data.statusName}`);
                                    });
                                    tooltipInstance.show(arg.target);
                                });

                                e.cellElement.mouseout(function (arg) {
                                    tooltipInstance.hide();
                                });
                            }
                        }
                    }
                },
                onEditingStart(e) {
                    if (e.key == 3) e.cancel = true;
                }
            }).dxDataGrid('instance');

            var tooltipInstance = $("#tooltipContainer").dxTooltip({
                position: "bottom"
            }).dxTooltip("instance");

            var textArea = $('#events').dxTextArea({
                height: 100,
                label: 'Мероприятия по возврату снижений',
                labelMode: 'floating',
                value: data.measures
            }).dxTextArea('instance');

            var button = $('#button').dxButton({
                text: 'На согласование',
                onClick(e) {
                    if (!isFormValid()) return;

                    var reasonId = selectBox.option('value');
                    var measures = textArea.option('value');
                    var qj = { value: dataGrid.cellValue(0, 'agreement') };
                    var wp = { value: dataGrid.cellValue(1, 'agreement') };
                    var nd = { value: dataGrid.cellValue(3, 'agreement') };
                    var rlin = { value: dataGrid.cellValue(4, 'agreement') };
                    var rbuf = { value: dataGrid.cellValue(5, 'agreement') };
                    var rzatr = { value: dataGrid.cellValue(6, 'agreement') };

                    var data = { depositId, reasonId, measures, qj, wp, nd, rlin, rbuf, rzatr };

                    fetch(`${url}/Data`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(data),
                    })
                        .then((response) => response.json())
                        .then((data) => {
                            dataGrid.option('dataSource', data.parameters);
                            disableForm(data.status);
                            $('#info').text(data.info);
                        })
                        .catch((error) => {
                            console.error('Error:', error);
                        });
                }
            }).dxButton('instance');

            function isFormValid() {
                dataGrid.cellValue(0, "agreement", dataGrid.cellValue(0, "agreement"));
                dataGrid.cellValue(1, "agreement", dataGrid.cellValue(1, "agreement"));
                var reasonValid = selectBox.element().dxValidator("instance").validate().isValid;
                var qjValid = dataGrid.getCellElement(0, "agreement").dxValidator("instance").validate().isValid;
                var wpValid = dataGrid.getCellElement(1, "agreement").dxValidator("instance").validate().isValid;

                return reasonValid && qjValid && wpValid;
            }

            function disableForm(status) {
                var disabled = status === 0;

                selectBox.option('disabled', disabled);
                dataGrid.option('editing', { allowUpdating: !disabled });
                textArea.option('disabled', disabled);
                button.option('disabled', disabled);
            }

            $('#deposit').text(data.depositName);
            $('#info').text(data.info);
            disableForm(data.status);
        });
    });