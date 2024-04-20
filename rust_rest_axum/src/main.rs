use axum::{
    routing::get,
    Router,
    response::{Json},
};
use std::sync::Arc;
use serde_json::{json};
use serde::{Serialize};

#[derive(Serialize)]
struct BarResponse {
    symbol: String,
    bars: Vec<Bar>,
}

#[derive(Clone, Copy, Serialize)]
struct Bar {
    open: f32,
    high: f32,
    low: f32,
    close: f32,
}

struct AppSate {
    bar100: BarResponse,
    bar5000: BarResponse,
}


#[tokio::main]
async fn main() {

    let symbol: &str = "AAPL";

    let bar100: BarResponse = BarResponse {
        symbol: symbol.to_string(),
        bars: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 100],
    };

    let bar5000: BarResponse = BarResponse {
        symbol: symbol.to_string(),
        bars: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 5000],
    };

    let shared_state = Arc::new(AppSate { bar100, bar5000 });

    // build our application with a single route
    let app = Router::new()
        .route("/", get(|| async { "Hello, World!" }))
        .route("/bar/100", get({
            Json(json!(Arc::clone(&shared_state).bar100))
        }))
        .route("/bar/5000", get(move || async move {
            Json(json!(Arc::clone(&shared_state).bar5000))
        }));

    // run our app with hyper, listening globally on port 3000
    let listener = tokio::net::TcpListener::bind("0.0.0.0:3005").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}