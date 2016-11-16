
function WLog(w, h) {
    var self = this;
    var p = document.createElement("div");
    p.style.position = "fixed";
    p.style.top = 0;
    p.style.width = w + 'px';
    p.style.height = h + 'px';
    p.style.background = 'rgba(0, 0, 0,0.3)';
    p.style.overflowY = 'auto';
    document.body.appendChild((p));

    var clearBtn = document.createElement("button");
    clearBtn.style.position = "fixed";
    clearBtn.style.top = h + 'px';
    clearBtn.style.left = 0;
    clearBtn.style.width = '50px';
    clearBtn.style.height = '30px';
    clearBtn.innerHTML = 'Clear';
    document.body.appendChild(clearBtn);
    clearBtn.onclick = function () {
        self.clear();
    }

    this.p = p;
    this.clearBtn = clearBtn;
    this.logging = true;

    window.onerror = function onerro(message, source, lineno, colno, error) {
        self.error("linenumber " + lineno + "\n" + message);
        return false;
    }
}
WLog.prototype.disable = function () {
    this.logging = false;
    this.p.style.display = 'none';
    this.clearBtn.display = 'none';
}
WLog.prototype.clear = function () {
    this.p.innerHTML = "";
}
WLog.prototype.debug = function (message) {
    if (!this.logging)
        return;
    var line = document.createElement("p");
    line.style.color = 'white';
    line.innerHTML = message;
    this.p.appendChild(line);
    this.p.scrollTop = this.p.scrollHeight;
}
WLog.prototype.error = function (message) {
    if (!this.logging)
        return;
    var line = document.createElement("p");
    line.style.color = 'red';
    line.innerHTML = message;
    this.p.appendChild(line);
    this.p.scrollTop = this.p.scrollHeight;
}
