class User extends React.Component {
    render() {
        const user = this.props.user;

        return (
            <div className="text-left">
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>User Id</strong></div>
                        <div className="text-display">{user.id}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Company Id</strong></div>
                        <div className="text-display">{user.companyId}</div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>Title</strong></div>
                        <div className="text-display">{user.title}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Forename</strong></div>
                        <div className="text-display">{user.forename}</div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-6">
                        <div><strong>Surname</strong></div>
                        <div className="text-display">{user.surname}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Date of Birth</strong></div>
                        <div className="text-display">{user.dateOfBirth}</div>
                    </div>
                </div>
                <div className="row text-muted">
                    <div className="col-sm-6">
                        <div><strong>Created</strong></div>
                        <div className="text-display">{user.createdDate}</div>
                    </div>
                    <div className="col-sm-6">
                        <div><strong>Last Changed</strong></div>
                        <div className="text-display">{user.lastChangedDate}</div>
                    </div>
                </div>
            </div>
        );
    }
}